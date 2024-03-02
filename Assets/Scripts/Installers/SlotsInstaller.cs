using DataBases;
using Slots;
using SlotsInterface;
using UnityEngine;
using Zenject;
using ISlotFactory = Slots.ISlotFactory;

namespace Installers
{
    public class SlotsInstaller : MonoInstaller
    {
        [SerializeField] private ElementsDB _elementsDB; 
        
        [SerializeField] private FieldController _fieldController;
        [SerializeField] private SlotParent _slotParent;
        public override void InstallBindings()
        {
            Container.Bind<IElementsDB>().FromInstance(_elementsDB).AsSingle();
            
            Container.Bind<SlotParent>().FromInstance(_slotParent).AsSingle();

            Container.Bind<ISlotFactory>().To<SlotFactory>().AsSingle();
            
            Container.Bind<IFieldController>().FromInstance(_fieldController).AsSingle();
        }
    }
}