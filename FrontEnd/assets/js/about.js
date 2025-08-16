document.addEventListener("DOMContentLoaded", function () {
    const chartData = [
      { year: "2008", value: 6 },
      { year: "2017", value: 13 },
      { year: "2020", value: 20 },
      { year: "2022", value: 28 },
      { year: "2024", value: 35 },
      { year: "2025", value: 40 },
    ];
  
    const chart = document.getElementById("companyChart");
    const maxHeight = 300;
    const maxValue = Math.max(...chartData.map((item) => item.value));
    const maxAxisValue = Math.ceil(maxValue * 1.1);
  
    chartData.forEach((item) => {
      const barHeight = (item.value / maxAxisValue) * maxHeight;
  
      const barContainer = document.createElement("div");
      barContainer.className = "bar-container";
  
      const bar = document.createElement("div");
      bar.className = "bar";
      bar.style.height = "0";
  
      const barValue = document.createElement("div");
      barValue.className = "bar-value";
      barValue.textContent = item.value + 'M';
  
      const yearLabel = document.createElement("div");
      yearLabel.className = "year-label";
      yearLabel.textContent = item.year;
  
      bar.appendChild(barValue);
      barContainer.appendChild(bar);
      barContainer.appendChild(yearLabel);
      chart.appendChild(barContainer);
  
      setTimeout(() => {
        bar.style.height = `${barHeight}px`;
      }, 100);
    });
  });