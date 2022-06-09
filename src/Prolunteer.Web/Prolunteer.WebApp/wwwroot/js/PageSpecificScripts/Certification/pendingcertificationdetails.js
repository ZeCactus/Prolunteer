$("document").ready(function () {
    var PDFJS = window['pdfjs-dist/build/pdf'];
    pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.6.347/pdf.worker.min.js';

    var userId = $("#userId").val();
    var certificationId = $("#certificationId").val();
    var pdfData;

    $.ajax({
        type: "POST",
        url: "/Certification/PendingCertificationDocument",
        data: { userId: userId, certificationId: certificationId}
    }).done(function (data) {
        $("#pdfDownloadLink").attr("href", `data:application/pdf;base64,${data}`);
        pdfData = atob(data);
        loadPdf();
    })
    var loadPdf = function () {
        var loadingTask = pdfjsLib.getDocument({ data: pdfData });
        loadingTask.promise.then(function (pdf) {
            console.log('PDF loaded');
            for (let pageNumber = 1; pageNumber <= pdf.numPages; pageNumber++) {
                pdf.getPage(pageNumber).then(function (page) {
                    console.log('Page loaded');
                    var viewport = page.getViewport({scale: 1});
                    var scale = $("#pdfDisplayArea").width() / viewport.width;
                    console.log(scale);
                    var viewport = page.getViewport({ scale: scale });

                    var canvas = document.createElement("canvas");
                    var context = canvas.getContext('2d');
                    canvas.height = viewport.height;
                    canvas.width = viewport.width;
                    $("#pdfDisplayArea").append(canvas);
                    var renderContext = {
                        canvasContext: context,
                        viewport: viewport
                    };
                    var renderTask = page.render(renderContext);
                    renderTask.promise.then(function () {
                        console.log('Page rendered');
                    });
                });
            }
        }, function (reason) {
            console.error(reason);
        });
    }
    window.addEventListener("resize", loadPdf());
})