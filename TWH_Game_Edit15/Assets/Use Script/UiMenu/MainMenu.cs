using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {      
        SceneManager.LoadScene("Map01");//����� Cut Scene ��說��� Scene ����� Cut Scene ᷹�ç�ǧ����ѹ���
        Time.timeScale = 1f;//�ѹ�������ͧ���

        //StartCoroutine(GameStart());//����� Cut Scene �Դ�ѹ������
    }

    //IEnumerator GameStart()
    //{
    //    yield return new WaitForSeconds(1.0f);//�ѹ���������� �����ҡѺ������� Cut Scene ��� �������ǧ���
    //    SceneManager.LoadScene("Map01");//�ѹ�������ͧ���
    //    Time.timeScale = 1f;//�ѹ�������ͧ���
    //}

    //public void LoadGame()
    //{
    //    SceneManager.LoadScene("Map02");
    //    Time.timeScale = 1f;
    //}   

    public void QuitGame()
    {
        Application.Quit();
    }
}
