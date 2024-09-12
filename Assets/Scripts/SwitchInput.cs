using UnityEngine;
using UnityEngine.UI;

public class SwitchInput : MonoBehaviour
{
    [SerializeField]
    private Sprite _spriteMouse;
    [SerializeField]
    private Sprite _spriteEye;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private GazeTracker _tracker;

    public void onButtonClick()
    {
        if(_image.sprite ==_spriteMouse)
        {
            _image.sprite = _spriteEye;
            _tracker.gazeSource = GazeTracker.GazeSource.UnitEye;
           
        } else
        {
            _image.sprite = _spriteMouse;
            _tracker.gazeSource = GazeTracker.GazeSource.Mouse;
        }
    }
}
