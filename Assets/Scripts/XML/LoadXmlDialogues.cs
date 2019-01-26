using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class LoadXmlDialogues : MonoBehaviour
{

    DiveDialogueClass diveDialogue;
    public TextAsset file;

    void Awake()
    {

        ///////////////////////////////////////////

        diveDialogue = XmlLoad<DiveDialogueClass>(file);
        Debug.Log("xml cargado: " + file.name);

        ///////////////////////////////////////////

        //DiveDialogueClass d_dialogue = new DiveDialogueClass();

        //DialogueTypeClass dialogueType1 = new DialogueTypeClass();
        //DialogueTypeClass dialogueType2 = new DialogueTypeClass();

        //dialogueType1.name = "MajorelBasic";
        //dialogueType2.name = "QuincarnonBasic";

        //DialogueNpcClass type1_npc1 = new DialogueNpcClass();
        //type1_npc1.name = "Mathias";
        //for (int i = 0; i < 3; i++)
        //{
        //    type1_npc1.dialogueLines.Add("Frase mathias " + i);
        //}

        //DialogueNpcClass type1_npc2 = new DialogueNpcClass();
        //type1_npc2.name = "Marga";
        //for (int i = 0; i < 3; i++)
        //{
        //    type1_npc2.dialogueLines.Add("Frase Marga " + i);
        //}

        //DialogueNpcClass type1_npc3 = new DialogueNpcClass();
        //type1_npc3.name = "Rosita";
        //for (int i = 0; i < 3; i++)
        //{
        //    type1_npc3.dialogueLines.Add("Frase Rosita " + i);
        //}

        //DialogueNpcClass type2_npc1 = new DialogueNpcClass();
        //type2_npc1.name = "Royce";
        //for (int i = 0; i < 3; i++)
        //{
        //    type2_npc1.dialogueLines.Add("Frase Royce " + i);
        //}

        //DialogueNpcClass type2_npc2 = new DialogueNpcClass();
        //type2_npc2.name = "Marga";
        //for (int i = 0; i < 3; i++)
        //{
        //    type2_npc2.dialogueLines.Add("Frase Marga " + i);
        //}

        //DialogueNpcClass type2_npc3 = new DialogueNpcClass();
        //type2_npc3.name = "Kip";
        //for (int i = 0; i < 3; i++)
        //{
        //    type2_npc2.dialogueLines.Add("Frase Kip " + i);
        //}

        //dialogueType1.npc.Add(type1_npc1);
        //dialogueType1.npc.Add(type1_npc2);
        //dialogueType1.npc.Add(type1_npc3);

        //dialogueType2.npc.Add(type2_npc1);
        //dialogueType2.npc.Add(type2_npc2);
        //dialogueType2.npc.Add(type2_npc3);

        //d_dialogue.dialogueTypes.Add(dialogueType1);
        //d_dialogue.dialogueTypes.Add(dialogueType2);

        //SaveXML<DiveDialogueClass>(Application.dataPath + "/Dialogue/", "DialogueTree_EngXXX.xml", d_dialogue);
        //print("xml salvado, ruta: " + Application.dataPath);

    }

    public static T XmlLoad<T>(TextAsset file) where T : DiveDialogueClass
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

    public DiveDialogueClass DClass
    {
        get
        {
            return diveDialogue;
        }

        set
        {
            diveDialogue = value;
        }
    }

}
