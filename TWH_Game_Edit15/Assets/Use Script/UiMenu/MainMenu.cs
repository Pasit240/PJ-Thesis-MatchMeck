using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {      
        SceneManager.LoadScene("CutScene");//ถ้ามี Cut Scene ใส่ชชื่อ Scene ที่มี Cut Scene แทนตรงในวงเล็บอันนี้
        Time.timeScale = 1f;//อันนี้ไม่ต้องยุ่ง
        StartCoroutine(DelaySce(40f));
        //StartCoroutine(GameStart());//ถ้ามี Cut Scene เปิดอันนี้ด้วย
    }

    //IEnumerator GameStart()
    //{
    //    yield return new WaitForSeconds(1.0f);//อันนี้ใส่เวลา ใส่เท่ากับเวลาเล่น Cut Scene เลย ให้ใส่ในวงเล็บ
    //    SceneManager.LoadScene("Map01");//อันนี้ไม่ต้องยุ่ง
    //    Time.timeScale = 1f;//อันนี้ไม่ต้องยุ่ง
    //}

    //public void LoadGame()
    //{
    //    SceneManager.LoadScene("Map02");
    //    Time.timeScale = 1f;
    //}   

    IEnumerator DelaySce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Map01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
