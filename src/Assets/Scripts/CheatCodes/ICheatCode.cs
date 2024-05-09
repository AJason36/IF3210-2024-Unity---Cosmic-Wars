public interface ICheatCode
{
    // Activate the cheat code, must be implemented by every scene that uses cheat codes
    // If it doesn't listen a certain cheat code, it can just ignore it (don't throw an exception)
    void ActivateCheatCode(CheatCodeManager.CheatCodes codes);
}
