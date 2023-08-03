using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoleData", menuName = "SO_Role")]
public class SO_Role : ScriptableObject
{
    public List<Role> roles = new List<Role>();
}
