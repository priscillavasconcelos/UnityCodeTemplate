using UnityEngine;
using UnityEditor;
using System.IO;

public class KeywordReplace : AssetModificationProcessor
{
    public static void OnWillCreateAsset ( string path )
    {
        path = path.Replace( ".meta", "" );
        int index = path.LastIndexOf( "." );
        string file = path.Substring( index );
        if ( file != ".cs" && file != ".js" && file != ".boo" ) return;
        index = Application.dataPath.LastIndexOf( "Assets" );
        path = Application.dataPath.Substring( 0, index ) + path;
        file = File.ReadAllText( path );
        string[] folders = Path.GetDirectoryName(path).Split("\\"); //System.IO.Directory.GetCurrentDirectory();
        string folder = folders[folders.Length - 1];
        folder = folder.Replace(" ", string.Empty);

        file = file.Replace( "#CREATIONDATE#", System.DateTime.Now + "" );
        file = file.Replace( "#PROJECTNAME#", PlayerSettings.productName.Replace(" ", string.Empty) );
        file = file.Replace( "#COMPANYNAME#", PlayerSettings.companyName.Replace(" ", string.Empty) );
        file = file.Replace( "#MODULENAME#", folder );

        File.WriteAllText( path, file );
        AssetDatabase.Refresh();
    }
}