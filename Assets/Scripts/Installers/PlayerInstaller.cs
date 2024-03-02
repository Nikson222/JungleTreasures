using System;
using DefaultNamespace;
using Player;
using PlayerInterfaces;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _view;

        [SerializeField] private int _startBalance;
        
        public override void InstallBindings()
        {
            //PlayerPrefs.DeleteAll();
            LastAddedBalanceDate lastAddedBalanceDate = new LastAddedBalanceDate();
            
            var balance = PlayerPrefs.GetInt("Balance");
            
            if(lastAddedBalanceDate.IsFirstDay)
                balance += _startBalance;
            
            if (lastAddedBalanceDate.Date.Year > 0 && lastAddedBalanceDate.Date != DateTime.Today)
                balance += _startBalance;
                
                
            Container.Bind<IPlayerView>().FromInstance(_view).AsSingle();
            
            Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle().WithArguments(balance);
            
            Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().NonLazy();

            Container.Bind<SlotsAnimator>().AsSingle();
        }
        
        private class LastAddedBalanceDate
        {
            public DateTime Date { get; set; }
            public bool IsFirstDay { get; set; }
            
            public LastAddedBalanceDate()
            {
                var day = PlayerPrefs.GetInt("Day");
                var month = PlayerPrefs.GetInt("Month");
                var year = PlayerPrefs.GetInt("Year", 0);

                if (year == 0)
                {
                    Date = DateTime.Today;
                    Save();
                    IsFirstDay = true;
                }
                else
                    Date = new DateTime(year, month, day);
            }

            public void Save()
            {
                Date = DateTime.Today;
                
                PlayerPrefs.SetInt("Day", Date.Day);
                PlayerPrefs.SetInt("Month", Date.Month);
                PlayerPrefs.SetInt("Year", Date.Year); 
            }
        }
    }
}