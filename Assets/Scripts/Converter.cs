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
        switch (element.GetModuleType())
        {
            case Element.ModuleType.LookCapsule:
                return CreateCapsuleModule("Modules/RCapsule/RCapsule", element);
            case Element.ModuleType.CommentCapsule:
                return CreateCapsuleModule("Modules/GCapsule/GCapsule", element);
            case Element.ModuleType.LikeCapsule:
                return CreateCapsuleModule("Modules/BCapsule/BCapsule", element);
            case Element.ModuleType.Base:
                Modules.Base baseModule = Instantiate(Resources.Load<Modules.Base>("Modules/Base/Base"));
                baseModule.Initialize(element);

                return baseModule;
            //case Inventory.Element.ModuleType.ResearchCenter:
            //    print("ResearchCenter");
            //    break;
        }

        return null;
    }

    private Modules.Capsule CreateCapsuleModule(string pathToPrefab, Inventory.Element element)
    {
        Modules.Capsule capsule = Instantiate(Resources.Load<Modules.Capsule>(pathToPrefab));
        capsule.Initialize((Capsule)element);

        return capsule;
    }
    #endregion

    #region Event handlers
    #endregion
}