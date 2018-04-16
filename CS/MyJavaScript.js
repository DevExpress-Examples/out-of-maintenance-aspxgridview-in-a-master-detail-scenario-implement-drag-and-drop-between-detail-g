var rowKey = -1;
var sourceGridCategoryID = -1
function OnControlsInitialized(s, e) {
    $('.draggableRow').draggable({
        helper: 'clone',
        start: function (ev, ui) {
            var $sourceElement = $(ui.helper.context);
            var $draggingElement = $(ui.helper);
            sourceGridCategoryID = ev.target.getAttribute("categoryID");
            var sourceGrid = ASPxClientGridView.Cast("detailGrid" + sourceGridCategoryID);

            $sourceElement.addClass("draggingStyle");
            $draggingElement.addClass("draggingStyle");
            $draggingElement.width(sourceGrid.GetWidth());

            rowKey = ev.target.getAttribute("productID");
        },
        stop: function (e, ui) {
            $(".draggingStyle").removeClass("draggingStyle");
        }
    });

    var settings = function () {
        return {
            tolerance: "intersect",
            drop: function (ev, ui) {
                $(".targetGrid").removeClass("targetGrid");

                var newCategoryID = ev.target.getAttribute("categoryID");
                if (newCategoryID != sourceGridCategoryID)
                    gridMaster.PerformCallback(rowKey + "|" + newCategoryID);
            },
            over: function (ev, ui) {
                if (ev.target.getAttribute("categoryID") != sourceGridCategoryID)
                    $(this).addClass("targetGrid");
            },
            out: function (ev, ui) {
                $(".targetGrid").removeClass("targetGrid");
            }
        };
    };

    $(".droppableGrid").droppable(settings());
}