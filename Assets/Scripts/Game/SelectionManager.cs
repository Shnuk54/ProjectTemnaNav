using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ISelectionResponse))]
[RequireComponent(typeof(IRayProvider))]
[RequireComponent(typeof(IRaySelector))]
public class SelectionManager : MonoBehaviour
{
    private ISelectionResponse _selectionResponse;
    private IRayProvider _rayProvider;
    private IRaySelector _selector;
  
  [SerializeField]  private Transform _currentSelection;
   [SerializeField] private Transform _selection;
    

    void Awake() {
        _selectionResponse = GetComponent<ISelectionResponse>();
        _rayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<IRaySelector>();
    }
    void Update() {
        if (_currentSelection != null) 
        { 
            _selectionResponse.OnDeselect(_currentSelection); 
        }

        var ray = _rayProvider.CreateRay();
        _selector.Check(ray);
        _currentSelection = _selector.GetSelection();
      
        if (_currentSelection != null) { 
            _selectionResponse.OnSelect(_currentSelection);
        }
    }
}
