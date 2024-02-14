using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    List<int> chart = new() {
        6,14,22,26,28,38,44,46,50,59,60,62,67,68,70,74,76,78,86,92,102,103,108,118,122,124,
        126,131,132,138,142,150,154,155,158,166,170,173,178,186,188,189,191,198,202,208,214,
        220,221,223,230,236,244,246,253,254,264,266,268,270,278,279,286,292,298,299,300,302,
        310,318,328,330,334,340,346,348,354,355,356,358,362,364,366,372,374,378,380,381,383,
        386,392,406,414,422,426,428,438,444,446,450,459,460,462,467,468,470,474,476,478,486,
        492,502,503,508,518,522,524,526,531,532,538,542,550,554,555,558,566,570,573,578,586,
        588,589,591,598,602,608,614,620,621,623,630,636,644,646,653,654,664,666,668,670,678,
        679,686,692,698,699,700,702,710,718,728,730,734,740,746,748,754,755,756,758,762,764,
        766,772,774,778,780,781,783,786,792
    };

    public int GetChart(int index)
    {
        return chart[index];
    }

    public int GetChartLength()
    {
        return chart.Count;
    }

    public void RemoveChart()
    {
        chart.RemoveAt(0);
    }
}
