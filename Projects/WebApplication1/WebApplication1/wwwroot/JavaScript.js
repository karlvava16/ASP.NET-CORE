function getWeather() {
    let city = document.getElementById("cityInput").value;
    let apiKey = "1b38763b24872342c3616a54ceb1c8a7"; // Replace "YOUR_API_KEY" with your actual API key from OpenWeatherMap
    let apiUrl = `http://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}`;

    let xhr = new XMLHttpRequest();
    xhr.open("GET", apiUrl, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            let response = JSON.parse(xhr.responseText);
            console.log(response);
            displayWeather(response);

            // Move the code that depends on response inside this callback function
            console.log(response.coord.lat);
            console.log(response.coord.lon);

            let apiUrl2 = `http://api.openweathermap.org/data/2.5/forecast?lat=${response.coord.lat}&lon=${response.coord.lon}&appid=${apiKey}`;

            let xhr2 = new XMLHttpRequest();
            xhr2.open("GET", apiUrl2, true);
            xhr2.onreadystatechange = function () {
                if (xhr2.readyState === 4 && xhr2.status === 200) {
                    let response2 = JSON.parse(xhr2.responseText);
                    displayWeatherList(response2);

                    console.log(response2);
                } else if (xhr2.readyState === 4 && xhr2.status !== 200) {
                    alert("Incorrect enter" + xhr2.status);
                }
            };
            xhr2.send();
        } else if (xhr.readyState === 4 && xhr.status !== 200) {
            alert("Incorrect enter" + xhr.status);
        }
    };
    xhr.send();

    document.getElementById("container").style.display = "flex";
}


function displayWeather(weatherData) {
    let cityNameH2 = document.getElementById("cityName");
    let weatherInfoDiv = document.getElementById("weatherInfo");
    let dateWeather = document.getElementById("date");
    let weatherType = document.getElementById("weatherType");
    let imgWeather = document.getElementById("imgWeather");
    let temperatureSpan = document.getElementById("temperature");
    let minTemperatureSpan = document.getElementById("minTemperature");
    let maxTemperatureSpan = document.getElementById("maxTemperature");
    let windSpeedSpan = document.getElementById("windSpeed");



    cityNameH2.textContent  = weatherData.name;
    dateWeather.textContent = SetDate(weatherData.dt);
    weatherType.textContent = weatherData.weather[0].main;
    imgWeather.textContent = weatherIcons[weatherData.weather[0].main];

    let temperature = Math.round(weatherData.main.temp - 273.15);
    temperatureSpan.innerHTML = `${temperature}°C`;

    let temperatureMin = Math.round(weatherData.main.temp_min - 273.15);
    minTemperatureSpan.innerHTML = `Min temperature: ${temperatureMin}°C`;

    let temperatureMax = Math.round(weatherData.main.temp_max - 273.15);
    maxTemperatureSpan.innerHTML = `Max temperature: ${temperatureMax}°C`;

    windSpeedSpan.innerHTML = `Wind Speed (km/h): ${weatherData.wind.speed}`;
    
}


function SetDate(unixTimestamp)
{
    let date = new Date(unixTimestamp * 1000);
    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    return day + '.' + month + '.' + year;
}


function displayWeatherList(weatherData) {

    let weatherInfo2 = document.getElementById("weatherInfo2")
    weatherInfo2.innerHTML = ""

    let additionalInfo2Div = document.createElement("div");
    additionalInfo2Div.classList.add("additionalInfo2");

    let div1 = document.createElement("div");
    let div2 = document.createElement("div");
    let div3 = document.createElement("div");
    let div4 = document.createElement("div");
    let div5 = document.createElement("div");

    let daydate = new Date(weatherData.list[0].dt * 1000);
    console.log(daydate);
    let h3 = document.createElement("h3");
    h3.textContent = dayNames[daydate.getDay()];
    div1.appendChild(h3);

    let span = document.createElement("span");
    span.classList.add("material-symbols-outlined");
    span.style.fontSize = "100px";
    span.style.width = "100px";
    span.style.height = "100px";
    span.style.color = "#3da4ab";
    div2.appendChild(span);

    let p1 = document.createElement("p");
    p1.textContent = "Forecast";
    let p2 = document.createElement("p");
    p2.textContent = `Temp(°C)`;
    let p3 = document.createElement("p");
    p3.textContent = "Wind (km/h)";
    div3.appendChild(p1);
    div4.appendChild(p2);
    div5.appendChild(p3);


    additionalInfo2Div.appendChild(div1);
    additionalInfo2Div.appendChild(div2);
    additionalInfo2Div.appendChild(div3);
    additionalInfo2Div.appendChild(div4);
    additionalInfo2Div.appendChild(div5);

    weatherInfo2.appendChild(additionalInfo2Div);


    for (let i = 0; i < weatherData.list.length && i < 5; i++) {
        let additionalInfo2Div = document.createElement("div");
        additionalInfo2Div.classList.add("additionalInfo2");

        let div1 = document.createElement("div");
        let div2 = document.createElement("div");
        let div3 = document.createElement("div");
        let div4 = document.createElement("div");
        let div5 = document.createElement("div");


        let h3 = document.createElement("h3");
        h3.textContent = convertUnixTimestampToTime(weatherData.list[i].dt);
        div1.appendChild(h3);

        let span = document.createElement("span");
        span.classList.add("material-symbols-outlined");
        span.style.fontSize = "100px";
        span.style.width = "100px";
        span.style.height = "100px";
        span.style.color = "#3da4ab";
        span.textContent = weatherIcons[weatherData.list[i].weather[0].main];
        div2.appendChild(span);

        let p1 = document.createElement("p");
        p1.textContent = weatherData.list[i].weather[0].main;
        let p2 = document.createElement("p");
        p2.textContent = `${Math.round(weatherData.list[i].main.temp - 273.15)}°C`;
        let p3 = document.createElement("p");
        p3.textContent = weatherData.list[i].wind.speed;
        div3.appendChild(p1);
        div4.appendChild(p2);
        div5.appendChild(p3);


        additionalInfo2Div.appendChild(div1);
        additionalInfo2Div.appendChild(div2);
        additionalInfo2Div.appendChild(div3);
        additionalInfo2Div.appendChild(div4);
        additionalInfo2Div.appendChild(div5);

        weatherInfo2.appendChild(additionalInfo2Div);
    }
}

function convertUnixTimestampToTime(unixTimestamp) {
    let milliseconds = unixTimestamp * 1000;

    let date = new Date(milliseconds);

    let hours = date.getHours();
    let minutes = "0" + date.getMinutes();
    let seconds = "0" + date.getSeconds();

    let time = hours + ':' + minutes.substr(-2);

    return time;
}
