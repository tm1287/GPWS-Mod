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
            if (activeVessel.speed > 5 || activeVessel.radarAltitude > activeVessel.terrainAltitude) {
                if (VesselAttributes.AoA(activeVessel) > 25) {
                    mode = GPWSMode.Stall;
                } else if (VesselAttributes.RadarAltitude(activeVessel) >= 150 && VesselAttributes.VerticalSpeed(activeVessel) < -40) {
                    mode = GPWSMode.RapidSink;
                } else if (VesselAttributes.RadarAltitude(activeVessel) < 150 && activeVessel.verticalSpeed < -10) {
                    mode = GPWSMode.TerrainPullUp;
                } else if (VesselAttributes.BankAngle(activeVessel) > 35) {
                    mode = GPWSMode.BankAngle;
                } else {
                    mode = GPWSMode.Nominal;
                }
            } else {
                mode = GPWSMode.Nominal;
            }

            switch (mode) {
                case GPWSMode.Nominal:
                    //TODO: Implement some kind of prediction system during nominal operation.
                    sm.StopSound();
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

        }
    }
}
