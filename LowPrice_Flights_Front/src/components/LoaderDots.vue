<template>
    <div class="loader_wrapper">
        <div id="loader">
            <div id="d1"></div>
            <div id="d2"></div>
            <div id="d3"></div>
            <div id="d4"></div>
            <div id="d5"></div>
        </div>
    </div>
</template>
  
<script>

</script>

<style>
.loader_wrapper {
    background: #00000050;
    position: absolute;
    width: 100vw;
    height: 100vh;
    top: 0;
    left: 0;
    z-index: 5;
    display: none;
}

#loader {
    width: 100px;
    height: 40px;
    position: absolute;
    top: 50%;
    left: 50%;
    margin: -20px -50px;
}

#loader div {
    width: 20px;
    height: 20px;
    background: #73ffec;
    border-radius: 50%;
    position: absolute;

}

#d1 {
    animation: animate 2s linear infinite;
}

#d2 {
    animation: animate 2s linear infinite -.4s;
}

#d3 {
    animation: animate 2s linear infinite -.8s;
}

#d4 {
    animation: animate 2s linear infinite -1.2s;
}

#d5 {
    animation: animate 2s linear infinite -1.6s;
}

@keyframes animate {
    0% {
        left: 100px;
        top: 0
    }

    80% {
        left: 0;
        top: 0;
    }

    85% {
        left: 0;
        top: -20px;
        width: 20px;
        height: 20px;
    }

    90% {
        width: 40px;
        height: 15px;
    }

    95% {
        left: 100px;
        top: -20px;
        width: 20px;
        height: 20px;
    }

    100% {
        left: 100px;
        top: 0;
    }
}
</style>