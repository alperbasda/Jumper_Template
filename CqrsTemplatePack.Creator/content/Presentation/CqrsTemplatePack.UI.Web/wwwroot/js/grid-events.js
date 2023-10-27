
var gridEvents = {
    qsGetParams: function (prop) {
        return new URLSearchParams(window.location.search).get(prop);
    },
    qsSetParams: function (prop, value) {
        return new URLSearchParams(window.location.search).set(prop, value);
    },
    init: function () {
        if ($('.dynamic-table').length == 0) return;

        gridEvents.setOrderQueries();
        gridEvents.setPaginationQueries();
    },
    setPaginationQueries: function () {

        for (var i = 0; i < $('[data-page]').length; i++) {
            var selected = $('[data-page]').get(i);
            var href = gridEvents.getNextPageQuery(selected);
            $(selected).attr('href', href);
        }

    },
    setOrderQueries: function () {

        for (var i = 0; i < $('[data-sort]').length; i++) {
            var selected = $('[data-sort]').get(i);
            var href = gridEvents.getNextOrderQuery(selected);
            $(selected).attr('href', href);
        }

    },
    getNextOrderQuery: function (item) {

        var urlParams = new URLSearchParams(window.location.search);
        urlParams.set('Page', 1);
        var sortProp = gridEvents.qsGetParams('Sort.Field');
        var selectedItemSortProperty = gridEvents._getItemSortProp(item);
        if (sortProp != selectedItemSortProperty) {
            urlParams.set('Sort.Field', selectedItemSortProperty);
            urlParams.set('Sort.OrderOperator', 'asc');
        }
        else {
            if (urlParams.get('Sort.OrderOperator') == 'asc') {
                urlParams.set('Sort.OrderOperator', 'desc');
                $(item).append('<i class="fa fa-solid fa-arrow-up"></i>');
            }
            else {
                urlParams.delete('Sort.OrderOperator');
                urlParams.delete('Sort.Field');
                $(item).append('<i class="fa fa-solid fa-arrow-down"></i>');
            }
        }
        return "?" + urlParams.toString();

    },
    sendFiltersAsync: function (form) {
        
        $(form).find('[data-loading]').replaceWith('<span class="spinner-border text-primary" role="status"></span>');
        
        var data = pageEvents.formToJSON($(form));
        var wrapper = $(form).closest('.partial-wrapper').children().remove();
        
        get($(form).attr('action'), data, function (htmlData) { $('.partial-wrapper').append(htmlData) });
    },

    getNextPageQuery: function (item) {
        var urlParams = new URLSearchParams(window.location.search);
        urlParams.set('Page', $(item).data('page'));
        return "?" + urlParams.toString();
    },
    _getItemSortProp: function (item) {
        return $(item).attr('data-sort');
    }

}
