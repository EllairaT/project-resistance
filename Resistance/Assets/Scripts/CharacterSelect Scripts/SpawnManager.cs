using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : SceneManager<SpawnManager>
{
    public PlayerScript[] characters;
    public GameObject spawnPoint;

    private int _currentIndex = 0;
    private PlayerScript _currentCharacterType = null;
    private PlayerScript _currentCharacter = null;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoint != null)
        {
            //SetCurrentCharacterType(_currentIndex);
        }
    }
    
    public void SetCurrentCharacterType(int index)
    {
        if (_currentCharacterType != null)
        {
            Destroy(_currentCharacterType.gameObject);
        }

        PlayerScript p = characters[index];
        _currentCharacterType = Instantiate<PlayerScript>(p, spawnPoint.transform.position, Quaternion.identity);       
        _currentIndex = index;
    }

    public void SetCurrentCharacterType(string n)
    {
        int i = 0;
        foreach(PlayerScript p in characters)
        {
            if (p.name.Equals(n, System.StringComparison.InvariantCultureIgnoreCase))
            {
                SetCurrentCharacterType(i);
                break;
            }
            i++;
        }
    }

    public void CreateCurrentCharacter(string n)
    {
        _currentCharacter = Instantiate<PlayerScript>(_currentCharacterType, spawnPoint.transform.position, Quaternion.identity);
        _currentCharacter.gameObject.SetActive(false);
        _currentCharacter.name = n;

        DontDestroyOnLoad(_currentCharacter);

        SceneManager.LoadScene(1);
    }

    public PlayerScript GetCurrentCharacter()
    {
        return _currentCharacter;
    }
}
