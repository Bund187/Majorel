using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class LoadXml_Narrator : MonoBehaviour {

    NarratorTextClass narratorClass;
    public TextAsset file;
   
    void Awake()
    {

        ///////////////////////////////////////////LOAD

        //narratorClass = XmlLoad<NarratorTextClass>(Application.dataPath + "/Resources/", "NarratorText_Esp.xml");
        narratorClass = XmlLoad<NarratorTextClass>(file);
        Debug.Log("xml cargado: " + file.name);
        ///////////////////////////////////////////SAVE

        //NarratorTextClass narratorClass = new NarratorTextClass();

        //for (int i = 0; i < 10; i++)
        //{
        //    narratorClass.sentence.Add("frase " + i);
        //    narratorClass.majorelSentence.Add("frase majorel " + i);
        //    narratorClass.entitySentence.Add("frase ente" + i);
        //}

        //SaveXML<NarratorTextClass>(Application.dataPath + "/Dialogue/", "NarratorText_Esp.xml", narratorClass);
        //print("xml salvado, ruta: " + Application.dataPath);

        ////////////////////////////////////////////
    }

    public static T XmlLoad<T>(TextAsset file) where T : NarratorTextClass
    {
        
        //Creamos la instancia de la clase XmlSerializer con el tipo deseado
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        //Verificamos si el archivo existe antes de intentar leerlo
        if (file!=null)
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

    //public static T XmlLoad<T>(string path, string fileName) where T : NarratorTextClass
    //{
    //    //Creamos la instancia de la clase XmlSerializer con el tipo deseado
    //    XmlSerializer serializer = new XmlSerializer(typeof(T));

    //    //Verificamos si el archivo existe antes de intentar leerlo
    //    if (File.Exists(path + fileName))
    //    {
    //        //Esta clase se encarga de la manipulación de archivos, creamos una instancia en modo abrir
    //        FileStream stream = new FileStream(path + fileName, FileMode.Open);

    //        //Creamos una instancia de StreamReader la clase que se encarga de leer el archivo
    //        StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);

    //        //Creamos una variable de tipo T (el tipo T representa el tipo que usamos al llamar el metodo)
    //        T t = serializer.Deserialize(sr) as T;

    //        //Cerramos el archivo para que esté disponible para otros procesos
    //        stream.Close();
    //        sr.Close();

    //        //Regresamos el objeto deserializado
    //        return t;
    //    }
    //    //Si no existe el archivo regresamos null indicando que no encontramos nada
    //    return null;
    //}

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

    public NarratorTextClass NarratorClass
    {
        get
        {
            return narratorClass;
        }

        set
        {
            narratorClass = value;
        }
    }
}


