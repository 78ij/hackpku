using UnityEngine;  
using UnityEngine.UI;  
  
public class discrete : RawImage {  
      
    private Slider _Slider;  
  
    protected override void OnRectTransformDimensionsChange()  
    {  
        base.OnRectTransformDimensionsChange();  

        if (_Slider == null)  
            _Slider = transform.parent.parent.GetComponent<Slider>();  

        if (_Slider != null)  
        {  
            float value = _Slider.value;  
            uvRect = new Rect(0,0,value,1);  
        }  
    }  
}  