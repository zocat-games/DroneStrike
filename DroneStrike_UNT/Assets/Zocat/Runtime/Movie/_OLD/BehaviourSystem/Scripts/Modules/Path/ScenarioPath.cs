using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Zocat
{
    public class ScenarioPath : InstanceBehaviour
    {
        public List<CheckPoint> CheckPoints;

        [Button(ButtonSizes.Medium)]
        public void SetVariables()
        {
            CheckPoints.Clear();
            CheckPoints = transform.parent.GetComponentsInChildren<CheckPoint>().ToList();
            for (var i = 0; i < CheckPoints.Count; i++)
            {
                var item = CheckPoints[i];
                item.Index = i;
                item.name = "CP" + i;
            }
        }
    }
}

// foreach (var item in CheckPoints)
// {
//     var code = item.transform.parent.gameObject.name;
//     var numberPart = Regex.Replace(code, "[^0-9]", "");
//     var value = int.Parse(numberPart.Substring(2));
//     item.gameObject.name = $"CheckPoint_{value}";
// }
// for (var i = 0; i < CheckPoints.Count; i++)
// {
// var item = CheckPoints[i];
// var code = item.transform.parent.gameObject.name;
// var numberPart = Regex.Replace(code, "[^0-9]", "");
// var value = int.Parse(numberPart.Substring(2));
// item.gameObject.name = $"CheckPoint_{i}";
// }