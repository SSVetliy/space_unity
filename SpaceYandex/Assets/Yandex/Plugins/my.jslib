mergeInto(LibraryManager.library, {

  SaveExtern: function(data) {
    var dataString = UTF8ToString(data);
    var myObj = JSON.parse(dataString);
    player.setData(myObj);
  },

  LoadExtern: function() {
    player.getData().then(_data => {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('Yandex', 'SetDATA', myJSON);
    });
  },

  AddCoinsExtern: function(value) {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('Yandex', 'AddMoneyAdv', value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },

  AddCoinsExtern2: function(value) {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('Yandex', 'AddMoneyAdv2', value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },

  ShowAdv:function (){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
           myGameInstance.SendMessage('Yandex', 'PlayBackgroundMusic');
        },
        onError: function(error) {
          // some action on error
        }
    }
})
  }

});