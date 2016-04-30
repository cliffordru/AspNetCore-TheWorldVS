// site.js

// Wrap in self executing annonymous function so we do not pollute global scope
(function (){
    var ele = document.getElementById("username");
    ele.innerHTML = "Clifford...";

    var main = document.getElementById("main");
    main.onmouseenter = function() {
        main.style = "background-color: #888;";
    };

    main.onmouseleave = function() {
        main.style = "";
    };
})();

