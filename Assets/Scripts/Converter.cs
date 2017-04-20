using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class Converter : Singleton<Converter>
{
	#region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    #endregion

    #region Classes
    #endregion

    #region Fiedls
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

	#region Constructors
	#endregion
	
    #region Methods
    public Module ConvertToModule(Element element)
    {
        Module module = null;
        switch (element.GetModuleType())
        {
            case Inventory.Element.ModuleType.LookCapsule:
                module = CreateCapsuleModule("Modules/RCapsule/RCapsule", element);
                break;
            case Inventory.Element.ModuleType.CommentCapsule:
                module = CreateCapsuleModule("Modules/GCapsule/GCapsule", element);
                break;
            case Inventory.Element.ModuleType.LikeCapsule:
                module = CreateCapsuleModule("Modules/BCapsule/BCapsule", element);
                break;
            //case Inventory.Element.ModuleType.Base:
            //    print("Base");
            //    break;
            //case Inventory.Element.ModuleType.ResearchCenter:
            //    print("ResearchCenter");
            //    break;
        }

        return module;
    }

    private Module CreateCapsuleModule(string pathToPrefab, Inventory.Element element)
    {
        Modules.Capsule capsule = Instantiate(Resources.Load<Modules.Capsule>(pathToPrefab));
        capsule.Initialize((Capsule)element);

        return capsule;
    }
    #endregion

    #region Event handlers
    #endregion
}