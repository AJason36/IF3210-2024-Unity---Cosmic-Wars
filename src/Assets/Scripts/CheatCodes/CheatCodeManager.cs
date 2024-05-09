using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
    public enum CheatCodes {
        NO_DAMAGE,
        ONE_HIT,
        INF_MONEY,
        SPEED_UP,
        IMMORTAL_PET,
        KILL_PET,
        INSTANT_ORB,
        SKIP_LEVEL,
    }

    // Map of cheat codes to their respective key codes
    private Dictionary<CheatCodes, string> cheatCodeMap = new Dictionary<CheatCodes, string>
    {
        { CheatCodes.NO_DAMAGE, "BAGUVIX" },
        { CheatCodes.ONE_HIT, "SAITAMA" },
        { CheatCodes.INF_MONEY, "MOTHERLODE" },
        { CheatCodes.SPEED_UP, "SONIC" },
        { CheatCodes.IMMORTAL_PET, "ANABUL" },
        { CheatCodes.KILL_PET, "MRFRESH" },
        { CheatCodes.INSTANT_ORB, "PLOTARMOR" },
        { CheatCodes.SKIP_LEVEL, "LAZYPLAYZY" },
    };

    // Singleton instance
    public static CheatCodeManager instance;
    
    // List of listeners for cheat codes
    private Dictionary<CheatCodes, ICheatCode[]> listeners = new Dictionary<CheatCodes, ICheatCode[]>();
    
    // Key presses variables
    private string buffer = "";
    private float lastKeyPressTime = 0;
    private const float MAX_TIME_BETWEEN_KEY_PRESSES = 0.3f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("There are multiple CheatCodeManagers in the scene. Destroying the new one.");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Time.time - lastKeyPressTime > MAX_TIME_BETWEEN_KEY_PRESSES)
            {
                ClearBuffer();
            }

            lastKeyPressTime = Time.time;
            buffer += Input.inputString.ToUpper();
            CheckCheatCode();
        }
    }

    private void CheckCheatCode()
    {
        foreach (var cheatCode in cheatCodeMap)
        {
            if (buffer.EndsWith(cheatCode.Value))
            {
                Debug.Log("Cheat code activated: " + cheatCode.Key);

                if (listeners.ContainsKey(cheatCode.Key))
                {
                    foreach (var listener in listeners[cheatCode.Key])
                    {
                        listener.ActivateCheatCode(cheatCode.Key);
                    }
                }

                ClearBuffer();
                return;
            }
        }
    }

    // Register 1 or more cheat code to a listener
    public void RegisterListener(ICheatCode listener, params CheatCodes[] codes)
    {
        foreach (var code in codes)
        {
            if (!listeners.ContainsKey(code))
            {
                listeners.Add(code, new ICheatCode[] { listener });
            }

            else
            {
                var list = new List<ICheatCode>(listeners[code])
                {
                    listener
                };

                listeners[code] = list.ToArray();
            }
        }
    }

    public void UnregisterListener(ICheatCode listener, params CheatCodes[] codes)
    {
        foreach (var code in codes)
        {
            if (listeners.ContainsKey(code))
            {
                var list = new List<ICheatCode>(listeners[code]);
                list.Remove(listener);
                listeners[code] = list.ToArray();
            }
        }
    }

    public void ClearListeners()
    {
        listeners.Clear();
    }

    private void ClearBuffer()
    {
        buffer = "";
    }
}
