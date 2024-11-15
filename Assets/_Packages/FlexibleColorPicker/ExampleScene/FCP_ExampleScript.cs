using UnityEngine;

namespace _Packages.FlexibleColorPicker.ExampleScene
{
    public class FCP_ExampleScript : MonoBehaviour {

        public bool getStartingColorFromMaterial;
        public global::FlexibleColorPicker fcp;
        public Material material;

        private void Start() {
            if(getStartingColorFromMaterial)
                fcp.color = material.color;

            fcp.onColorChange.AddListener(OnChangeColor);
        }

        private void OnChangeColor(Color co) {
            material.color = co;
        }
    }
}
