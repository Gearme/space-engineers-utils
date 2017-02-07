public void Main(string argument) {
    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

    List<IMyTerminalBlock> statusPanelsTerminals = new List<IMyTerminalBlock>();
     GridTerminalSystem.SearchBlocksOfName("Refinery Status", statusPanelsTerminals);
    List<IMyTextPanel> statusPanels = new List<IMyTextPanel>();
    foreach(IMyTerminalBlock terminal in statusPanelsTerminals){
        if (terminal is IMyTextPanel){
            Echo("found panel!");
            statusPanels.Add((IMyTextPanel)terminal);
        }
    }
    
    

    List<IMyRefinery> refineries = new List<IMyRefinery>();    
     GridTerminalSystem.GetBlocksOfType<IMyRefinery>(refineries); 
     
    List<IMyRefinery> idleRefineries = new List<IMyRefinery>();
    
    foreach(IMyRefinery refinery in refineries){
    string name = refinery.CustomName.Substring(9);
        if(!refinery.IsProducing){
            if(refinery.IsQueueEmpty){
                stringBuilder.Append(name + " reports: Empty queue!");
            }
            else{
                stringBuilder.Append(name + " reports: Unable to process!");
            }
            stringBuilder.AppendLine();
        }
    }
    
    
    foreach(IMyTextPanel statusPanel in statusPanels){
        statusPanel.WritePublicText(stringBuilder.ToString(), false);
        statusPanel.ShowPublicTextOnScreen();
    }
    
    Echo(stringBuilder .ToString());
}