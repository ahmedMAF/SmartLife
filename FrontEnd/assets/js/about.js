
document.addEventListener('DOMContentLoaded', function () {
    const chartData = [
        { year: "2008", value: 6 },
        { year: "2017", value: 13 },
        { year: "2020", value: 20 },
        { year: "2022", value: 28 },
        { year: "2024", value: 35 }
    ];

    const chart = document.getElementById('companyChart');
    const maxHeight = 300; // Adjusted for the container height

    // Create Y-axis
    const yAxis = document.createElement('div');
    yAxis.className = 'y-axis';
    yAxis.innerHTML = `
          <div class="y-tick"><span class="y-tick-label">35</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">30</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">25</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">20</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">15</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">10</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">5</span><span class="y-tick-line"></span></div>
          <div class="y-tick"><span class="y-tick-label">0</span><span class="y-tick-line"></span></div>
      `;
    chart.appendChild(yAxis);

    // Find maximum value for scaling
    const maxValue = Math.max(...chartData.map(item => item.value));

    chartData.forEach(item => {
        const barHeight = (item.value / maxValue) * maxHeight;

        const barContainer = document.createElement('div');
        barContainer.className = 'bar-container';

        const bar = document.createElement('div');
        bar.className = 'bar';
        bar.style.height = '0'; // Start from 0 for animation

        const barValue = document.createElement('div');
        barValue.className = 'bar-value';
        barValue.textContent = item.value;

        const yearLabel = document.createElement('div');
        yearLabel.className = 'year-label';
        yearLabel.textContent = item.year;

        bar.appendChild(barValue);
        barContainer.appendChild(bar);
        barContainer.appendChild(yearLabel);
        chart.appendChild(barContainer);

        // Animate the bar after a short delay
        setTimeout(() => {
            bar.style.height = `${barHeight}px`;
        }, 100);
    });
});