using System;
using UnityEngine;

namespace GPWS
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class GPWSModule : MonoBehaviour {

        public Vessel activeVessel;
        private enum GPWSMode {Nominal, BankAngle, RapidSink, Stall, TerrainPullUp};
        private GPWSMode mode;
        private SoundManager sm; 
        public void Start() {
            sm = gameObject.GetComponent<SoundManager>() ?? gameObject.AddComponent<SoundManager>();
        }

        public void Update() {
            activeVessel = FlightGlobals.ActiveVessel;
            
            if (activeVessel.radarAltitude >= 150 && activeVessel.verticalSpeed < -40) {
                mode = GPWSMode.RapidSink;
            } else if (activeVessel.radarAltitude < 150 && activeVessel.verticalSpeed < -10) {
                mode = GPWSMode.TerrainPullUp;
            } 
            else {
                mode = GPWSMode.Nominal;
            }

            switch (mode) {
                case GPWSMode.Nominal:
                    //TODO: Implement some kind of prediction system during nominal operation.
                    break;
                case GPWSMode.BankAngle:
                    sm.PlaySound(sm.bankAngleWarning, 1.0f);
                    break;
                case GPWSMode.RapidSink:
                    sm.PlaySound(sm.dontSinkWarning, 1.0f);
                    break;
                case GPWSMode.Stall:
                    sm.PlaySound(sm.stallWarning, 1.0f);
                    break;
                case GPWSMode.TerrainPullUp:
                    sm.PlaySound(sm.terrainPullUpWarning, 1.0f);
                    break;
            }

            //Debug.Log(Vector3d.Angle(activeVessel.srf_vel_direction, (Vector3d) activeVessel.terrainNormal));
        }
    }
}
