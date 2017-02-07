//Setting sensor action arguments:
//when criteria in range: <groupName>;<something>
//when criteria leaves space: <groupName>;<somethingDifferent>
//Example: 
//Arguments: 1;in, 1;out
//Light group name: *Light 1*
static Color occupiedColor = new Color(196,48,48);  
static Color FreeColor = new Color(50,149,45);  
 
bool state;  
List<IMyInteriorLight> lights; 
  
public Program() { 
    //Load current State  
    if (!bool.TryParse(Storage, out state)) { // Couldnt parse -> default Value false  
        state = false; 
    }  
}  
  
public void Save() {  
    //Save state variable 
    Storage = Convert.ToString(state);    
}  
  
public void Main(string argument) { 
    string[] arguments = argument.Split(';');
    prepareLightGroup(arguments[0]);
    // If Lights are green (!state) and need to be changed to red (isActive()) 
    if(!state){ 
        setColor(occupiedColor, true);  
    // If Lights are red and need to be changed to green 
    }else if(state){       
        setColor(FreeColor, false);  
    }  
} 
  
// prepare the lights  
public void prepareLightGroup(string groupName){  
    var lightsGroup = GridTerminalSystem.GetBlockGroupWithName("Light " + groupName);  
    lights = new List<IMyInteriorLight>(); 
    lightsGroup.GetBlocksOfType(lights); 
} 
 
public void setColor(Color color, bool newState){ 
    foreach (IMyInteriorLight light in lights){ 
        light.SetValue("Color", color); 
    } 
    state = newState; 
}
