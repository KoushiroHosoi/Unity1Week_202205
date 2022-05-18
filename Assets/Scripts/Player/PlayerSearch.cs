using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    public delegate void SearchingBook();
    public SearchingBook onSearchingBook;
    public SearchingBook onExitSearching;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Book(Clone)")
        {
            onSearchingBook();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Book(Clone)")
        {
            onExitSearching();
        }
    }
}
