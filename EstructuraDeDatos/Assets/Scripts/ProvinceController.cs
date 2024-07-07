using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvinceController : MonoBehaviour
{
    [SerializeField] private Province selectedProvince;

    public void SetSelectedProvince(Province province)
    {
        selectedProvince = province;
    }

    public Province GetSelectedProvince()
    {
        return selectedProvince;
    }
}
