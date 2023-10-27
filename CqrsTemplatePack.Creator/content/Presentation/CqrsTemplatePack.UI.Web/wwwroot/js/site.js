
$(function () {
    notificationEvents.backNotification();
    gridEvents.init();
    pageEvents.loadPartials(function () {
        pageEvents.setDynamicDropdowns();
        pageEvents.setDynamicReferences();
    });
    
});

$(document).on('click', '.delete-link',function (e) {
    var item = this;
    e.preventDefault();
    e.stopPropagation();
    messageOptions = {
        Type: "question",
        Title: "Silme İşlemi",
        Description: "Veri Silinecek Emin Misiniz ?",
        ThenTrue: function () {
            get($(item).attr('href'), null, function () {
                $(item).closest('.item_wrapper').remove();
                notificationEvents.showInfo("İşlem Başarılı");

            });
            
        }
    };
    messageEvents.show(messageOptions);
});

