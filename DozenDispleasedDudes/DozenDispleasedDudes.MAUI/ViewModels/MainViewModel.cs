using DozenDispleasedDudes;
using DozenDispleasedDudes.Models;//THISSSS
using DozenDispleasedDudes.Services;// MAKES NO SENSSEEE
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;


// NEED TO GO through and see which helper functions have console Commands 
//ClientService.Get and by extension anything that uses it needs to be refactored
//Also ClientHelper and ProjectHelper are completly unusable
//Which is extremely unfortunate because its the main way we handle empty client roster empty portfolio
//Aswell as how the default Constructor for both services work 
// Most recent Chages to main view Model not handeling Update
namespace DozenDispleasedDudes.MAUI.ViewModels
{
    internal class MainViewModel 
    {
       public List<Client> Roster { get; set; } = new List<Client>();

    }
}

