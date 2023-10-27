function get(route, params, callback) {
    $.ajax({
        url: getBaseUrl() + route,
        type: 'GET',
        data: params,
        success: function (data) {
            
            if (data.errors != undefined) {
                notificationEvents.showError(data.errors);
                return;
            }
            if (callback)
                callback(data);
        },
        fail: function (ex) {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        error: function (ex) {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        complete: function () {
            
        }
    });
}

function post(route, params, callback) {
    $.ajax({
        url: getBaseUrl() + route,
        type: 'POST',
        data: params,
        success: function (data) {
            if (data.errors != undefined) {
                notificationEvents.showError(data.errors);
                return;
            }
            if (callback)
                callback(data);
        },
        fail: function (ex) {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        error: function (ex) {
            notificationEvents.showError("İşlem Sırasında Hata Oluştu");
        },
        complete: function () {
            
        }
    });
}

function getBaseUrl() {
    var getUrl = window.location;
    return getUrl.protocol + "//" + getUrl.host;
}

function getUrlWithoutQs() {
    return window.location.href.split('?')[0];
}

function groupBy(list, keyGetter) {
    
    const map = new Map();
    list.forEach((item) => {
        const key = keyGetter(item);
        const collection = map.get(key);
        if (!collection) {
            map.set(key, [item]);
        } else {
            collection.push(item);
        }
    });
    return map;
}