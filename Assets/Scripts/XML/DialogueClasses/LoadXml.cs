using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class LoadXml : MonoBehaviour {

    //DialogueClass dClass;
    Dialogue_ConversationClass d_Conversation;
    public TextAsset file;

    void Awake()
    {

        ///////////////////////////////////////////

        d_Conversation = XmlLoad<Dialogue_ConversationClass>(file);
        Debug.Log("xml cargado: " + file.name);

        ///////////////////////////////////////////

        //Dialogue_ConversationClass d_Conversation = new Dialogue_ConversationClass();
        //Dialogue_UniqueQuestionClass d_UniqueClass = new Dialogue_UniqueQuestionClass();
        //Dialogue_UniqueQuestionClass d_UniqueClass2 = new Dialogue_UniqueQuestionClass();
        //Dialogue_UniqueQuestionClass d_UniqueClass3 = new Dialogue_UniqueQuestionClass();
        //Dialogue_GreetingClass d_GreetingClass = new Dialogue_GreetingClass();
        //Dialogue_RumourClass d_RumourClass = new Dialogue_RumourClass();

        //for (int i = 0; i < 5; i++)
        //{
        //    d_GreetingClass.greetingsAnswers.Add("Respuesta Saludo " + i);
        //    d_GreetingClass.greetingsQuestions.Add("¿Saludo " + i);

        //    d_RumourClass.rumoursAnswers.Add("Respuesta rumor " + i);
        //    d_RumourClass.rumoursQuestion.Add("¿Rumor " + i);
        //}

        //d_UniqueClass.speakers = 0;
        //d_UniqueClass.listener = 2;
        ////d_UniqueClass.trigger = false;
        //d_UniqueClass.keys.Clear();

        //for (int i = 0; i < 3; i++)
        //{
        //    d_UniqueClass.uniqueQuestion.Add("Pregunta unica de Majorel " + i);
        //    d_UniqueClass.uniqueAnswers.Add("Respuesta unica a Pregunta unica de Majorel " + i);
        //}

        //d_UniqueClass2.speakers = 15;
        //d_UniqueClass2.listener = -1;
        ////d_UniqueClass2.trigger = true;
        //d_UniqueClass2.keys.Add("lover");
        //for (int i = 0; i < 2; i++)
        //{
        //    d_UniqueClass2.uniqueQuestion.Add("Pregunta unica de Rosita a Quincarnon " + i);
        //    d_UniqueClass2.uniqueAnswers.Add("Respuesta unica de Quincarnon a Rosita " + i);
        //}

        //d_UniqueClass3.speakers = -1;
        //d_UniqueClass3.listener = -1;
        ////d_UniqueClass3.trigger = true;
        //d_UniqueClass3.keys.Add("pidgeon");
        //d_UniqueClass3.keys.Add("address");
        //d_UniqueClass3.uniqueQuestion.Add("Tienes palomas mensajeras?");
        //d_UniqueClass3.uniqueQuestion.Add("Como contacto con esta direccion?");
        //d_UniqueClass3.uniqueAnswers.Add("No, pero seguro que el granjero tiene");
        //d_UniqueClass3.uniqueAnswers.Add("Con una paloma mensajera");

        //d_Conversation.greetingClass = d_GreetingClass;
        //d_Conversation.rumourClass = d_RumourClass;
        //d_Conversation.uniqueClass.Add(d_UniqueClass);
        //d_Conversation.uniqueClass.Add(d_UniqueClass2);
        //d_Conversation.uniqueClass.Add(d_UniqueClass3);

        //d_Conversation.returnLine = "Adios";
        //d_Conversation.needInformation = "Recabar información";
        //d_Conversation.playMeoan = "Juguemos al Meoan";
        //d_Conversation.shop = "¿Qué tienes a la venta?";
        //d_Conversation.wantToAsk = "Quiero preguntarte una cosa...";

        //foreach (Dialogue_UniqueQuestionClass DC in d_Conversation.uniqueClass)
        //{
        //    print("Speaker ->" + DC.speakers);
        //}

        //SaveXML<Dialogue_ConversationClass>(Application.dataPath + "/Dialogue/", "NewDialogueTest3.xml", d_Conversation);
        //print("xml salvado, ruta: " + Application.dataPath);

    }

    public static T XmlLoad<T>(TextAsset file) where T : Dialogue_ConversationClass
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

    public Dialogue_ConversationClass DClass
    {
        get
        {
            return d_Conversation;
        }

        set
        {
            d_Conversation = value;
        }
    }

}
