using Gisha.Effects.Audio;
using Gisha.Effects.VFX;
using UnityEngine;

namespace Gisha.JamTools.Test
{
    public class TestInput : MonoBehaviour
    {
        public string sfxName;
        public string vfxName;

        Camera cam;

        void Awake()
        {
            cam = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) OnClick();
        }

        void OnClick()
        {
            AudioManager.Instance.PlaySFX(sfxName);

            RaycastHit hitInfo;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
                VFXManager.Instance.PoolEmit(vfxName, hitInfo.point, Quaternion.identity);
        }
    }
}