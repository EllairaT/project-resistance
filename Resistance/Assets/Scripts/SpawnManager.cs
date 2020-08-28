using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : SceneManager<SpawnManager>
{
    public Player[] characters;
    public GameObject spawnPoint;

    private int _currentIndex = 0;
    private Player _currentCharacterType = null;
    private Player _currentCharacter = null;

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

        Player p = characters[index];
        _currentCharacterType = Instantiate<Player>(p, spawnPoint.transform.position, Quaternion.identity);       
        _currentIndex = index;
     
    }

    public void SetCurrentCharacterType(string n)
    {
        int i = 0;
        foreach(Player p in characters)
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
        _currentCharacter = Instantiate<Player>(_currentCharacterType, spawnPoint.transform.position, Quaternion.identity);
        _currentCharacter.gameObject.SetActive(false);
        _currentCharacter.name = n;

        DontDestroyOnLoad(_currentCharacter);

        SceneManager.LoadScene(1);
    }

    public Player GetCurrentCharacter()
    {
        return _currentCharacter;
    }
}
