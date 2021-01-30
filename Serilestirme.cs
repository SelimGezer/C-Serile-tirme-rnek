using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class KarakterData {///Karakter Verileri
//karakterin adı
  public string karakterAdi;
    //pos
  public float pos_x;
  public float pos_y;
  public float pos_z;

}

/// <summary>
/// //Yükleme-Kaydetme Noktası
/// </summary>
public static class SaveLoadManager
{
     public static void SavePlayer(KarakterData karakterData){
            BinaryFormatter bf=new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/player.txt", FileMode.Create);

            bf.Serialize(fileStream, karakterData);
            fileStream.Close();
        }

    public static KarakterData LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/player.txt", FileMode.Open);
            
            KarakterData karakterData = (KarakterData)bf.Deserialize(fileStream);

            fileStream.Close();

            return karakterData;
        }
        else
            return null;
    }
}


public class Serilestirme : MonoBehaviour
{
    public GameObject karakter;
    public string ad;

    private void Start()
    {
        Debug.Log("Dosya Yolu:" + Application.persistentDataPath);
    }

    public void kaydet_B() {
        KarakterData karakterData = new KarakterData();

        //Ad Kaydet
        karakterData.karakterAdi = ad; //Inspectordan ad gir
        //Pozisyon Kaydet
        karakterData.pos_x = karakter.transform.position.x;
        karakterData.pos_y = karakter.transform.position.y;
        karakterData.pos_z = karakter.transform.position.z;
        
        SaveLoadManager.SavePlayer(karakterData);///ad- pos
    }

    public void yukle_B() {
        //Dosyadan Yüklenen KarakterDatayı Al 
        KarakterData karakterData=SaveLoadManager.LoadPlayer();
       
        //Karakteri Kayıtlı Pozisyona Al
        karakter.transform.position = new Vector3(karakterData.pos_x, karakterData.pos_y, karakterData.pos_z);
      
        //Karaktere Kayıtlı İsmi Bastir
        karakter.GetComponentInChildren<TextMeshProUGUI>().text = karakterData.karakterAdi;
    }
}
