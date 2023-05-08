using System.Collections.Generic; 
using UnityEngine.InputSystem; 
public class InputManager 
{ 
    private static BaseInput input; 
    public static BaseInput Input 
    { 
        private set => input = value; 
        get { 
            if (!isInitialized) 
                InitReference();         
            return input; 
        } 
    } 
    public static bool isInitialized = false; 
    private static void InitReference() { 
        Input = new StandardInputs(); 
        isInitialized = true; 
    } 
} 
public abstract class BaseInput : Inputs 
{ 
    protected InputState currentState = InputState.None; 
    protected Dictionary<InputState, List<InputAction>> inputs = new Dictionary<InputState, List<InputAction>>(); 
    protected BaseInput() { 
        Enable(); 
    } 
    public void OnSwitchInput(InputState inputState) { 
        if (!inputs.ContainsKey(inputState)) return; 
        if (inputs.ContainsKey(currentState))  
            foreach (var input in inputs[currentState])  
                input.Disable(); 
        currentState = inputState; 
        foreach (var input in inputs[currentState])  
            input.Enable(); 
    } 
    public enum InputState 
    { 
        None,
        Gameplay 
    } 
} 
public class StandardInputs : BaseInput 
{ 
    public StandardInputs() { 
        var gameplayActions = new List<InputAction>(); 
        gameplayActions.Add(Character.Movement); 

        inputs.Add(InputState.Gameplay, gameplayActions); 
    } 
}