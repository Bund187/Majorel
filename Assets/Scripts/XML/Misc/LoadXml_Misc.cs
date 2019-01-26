using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class LoadXml_Misc : MonoBehaviour
{

    MiscClass miscClass;
    public TextAsset file;
    void Awake()
    {

        ///////////////////////////////////////////LOAD

        miscClass = XmlLoad<MiscClass>(file);
        Debug.Log("xml cargado: " + file.name);

        ///////////////////////////////////////////SAVE

        //MiscClass miscClass = new MiscClass();

        //miscClass.gold = "gold";
        //miscClass.placeYourbet = "Place your bet";
        //miscClass.bet = "Bet";
        //miscClass.exit = "Exit";
        //miscClass.rolltheDice = "Rol the dice";
        //miscClass.roll = "Roll";
        //miscClass.pressAnykey = "Press Any Key";
        //miscClass.player = "Player";
        //miscClass.opponent = "Opponent";
        //miscClass.score = "Score";
        //miscClass.roundScore = "Round Score";
        //miscClass.pass = "Pass";
        //miscClass.youWin = "You Win!";
        //miscClass.youLose = "You Lose";
        //miscClass.warningBet = "You must bet some gold";
        //miscClass.playerStarts = "Player Starts";
        //miscClass.opponentStarts = "Opponent Starts";
        //miscClass.draw = "Draw";
        //miscClass.rollAgain = "Roll Again";
        //miscClass.badLuck = "Bad luck";
        //miscClass.opponentFail = "Opponent fails";
        //miscClass.yourTurn = "Your turn";
        //miscClass.luckyOne = "Lucky One!!";
        //miscClass.opponenPasses = "Opponent Passes";
        //miscClass.yourEarn = "You earn ";

        //miscClass.bottle = "Alcohol bottle";
        //miscClass.shopSentence = "What do you have for sale?";

        //SaveXML<MiscClass>(Application.dataPath + "/Dialogue/", "MiscText_Eng.xml", miscClass);
        //print("xml salvado, ruta: " + Application.dataPath);

        ////////////////////////////////////////////
    }

    public static T XmlLoad<T>(TextAsset file) where T : MiscClass
    {

        //Creamos la instancia de la clase XmlSerializer con el tipo deseado
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        //Verificamos si el archivo existe antes de intentar leerlo
        if (file != null)
        {
            //Creamos una instancia de StreamReader la clase que se encarga de leer el archivo
            StringReader sr = new StringReader(file.ToString());

            //Creamos una variable de tipo T (el tipo T representa el tipo que usamos al llamar el metodo)
            T t = serializer.Deserialize(sr) as T;

            //Cerramos el archivo para que esté disponible para otros procesos
            sr.Close();

            //Regresamos el objeto deserializado
            return t;
        }
        //Si no existe el archivo regresamos null indicando que no encontramos nada
        Debug.LogWarning("Archivo no encontrado");
        return null;
    }

    public static void SaveXML<T>(string path, string fileName, object data) where T : class
    {
        //Verificamos que la ruta de guardado exista antes de intentar guardar el archivo, sino existe esta es creada
        Directory.CreateDirectory(path).Create();

        //Creamos la instancia de la clase XmlSerializer con el tipo deseado
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        //Agregamos la extensión .xml al nombre del archivo si este no lo contiene
        if (!fileName.Contains(".xml"))
            fileName += ".xml";

        //Esta clase se encarga de la manipulación de archivos, creamos una instancia en modo crear, 
        //de esta manera si no existe el archivo se crea, usando la ruta y el nombre del archivo especificada
        FileStream stream = new FileStream(path + fileName, FileMode.Create);

        //Esta clase es la que se encargará de escribir el contenido en el archivo
        StreamWriter streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
        serializer.Serialize(streamWriter, data);

        //Cerramos el archivo para que esté disponible para otros procesos
        stream.Close();
        streamWriter.Close();
    }

    public MiscClass MiscClass
    {
        get
        {
            return miscClass;
        }

        set
        {
            miscClass = value;
        }
    }
}