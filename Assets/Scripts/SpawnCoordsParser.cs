using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using OVRSimpleJSON;
using System.IO;
using System;

public class SpawnCoordsParser
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public string Name { get; set; }
}