bool state; 
List<IMyInteriorLight>[] lights; 
IMySensorBlock[] sensors;
string oldArgument;
 
public Program() { 
    string[] storedParameter = Storage.Split(';'); 
    //Load current State 
    if (!bool.TryParse(storedParameter[0], out state)) { // Couldnt parse -> default Value false 
        state = false;
    } 
     
    //Load old Parameters 
    if(storedParameter.Length<=1){ // List of Parameters too short -> default Value "" 
        oldArgument = ""; 
    }else{ 
        oldArgument = storedParameter[1]; 
    } 
} 
 
public void Save() { 
    Storage = oldArgument + ";" + Convert.ToString(state);   
} 
 
public void Main(string argument) {
    bool changedArg = false;
    if (argument != oldArgument && argument != ""){ //New Groups defined 
        initOnDemand(argument); 
        oldArgument = argument;
        changedArg = true;
    } 
    Echo("Found Lights: " + lights[0].Count + "\nSensor: " + (sensors.Length == 1) + "\nNew Args: " + changedArg);
} 
 
// Prepares the Sensor- and Lightreferenzes based on the argument 
public void initOnDemand(string argument){ 
    string[] groups = argument.Split(';'); 
    prepareSensors(groups); 
    prepareLightGroups(groups); 
} 
 
// Get the Sensor of a specific Group 
public void prepareSensors(string[] groups){ 
    string[] sensorNames = new string[groups.Length]; 
    sensors = new IMySensorBlock[groups.Length]; 
     
    for (int i = groups.Length-1; i >= 0; i--){
        Echo("Sensor " + groups[i]);
        sensors[i] = GridTerminalSystem.GetBlockWithName("Sensor " + groups[i]) as IMySensorBlock; 
    } 
} 
 
// Get the lights of a specific Group 
public void prepareLightGroups(string[] groups){ 
    string[] LightGroupNames = new string[groups.Length]; 
    lights = new List<IMyInteriorLight>[groups.Length]; 
     
    for (int i = groups.Length-1; i >= 0; i--){
        Echo("Light " + groups[i]);
        var lightsGroup = GridTerminalSystem.GetBlockGroupWithName("Light " + groups[i]); 
        lights[i] = new List<IMyInteriorLight>();
        lightsGroup.GetBlocksOfType(lights[i]);
    } 
}