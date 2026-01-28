document.addEventListener("DOMContentLoaded", function () {
  const chart = document.getElementById("companyChart");
  const maxHeight = 300;
  const maxValue = Math.max(...chartData.map((item) => item.value));
  const maxAxisValue = Math.ceil(maxValue * 1.1);

  chartData.forEach((item) => {
    const barHeight = (item.value / maxAxisValue) * maxHeight;

    const barContainer = document.createElement("div");
    barContainer.className = "bar-container";

    const bar = document.createElement("div");
    bar.className = "chart-bar bar";
    bar.style.height = "0";

    const barValue = document.createElement("div");
    barValue.className = "bar-value";
    barValue.textContent = item.value + "M";

    const yearLabel = document.createElement("div");
    yearLabel.className = "year-label";
    yearLabel.textContent = item.year;

    barContainer.appendChild(barValue);
    barContainer.appendChild(bar);
    barContainer.appendChild(yearLabel);
    chart.appendChild(barContainer);

    setTimeout(() => {
      bar.style.height = `${barHeight}px`;
    }, 100);
  });
});











document.addEventListener("DOMContentLoaded", () => {
  const counters = document.querySelectorAll(".counter"); // كل الـ spans

  // دالة لتشغيل العد
  function animateCounter(counter) {
    const target = parseInt(counter.textContent.replace(/\D/g, ""), 10); // الرقم النهائي
    let count = 0;
    const duration = 2000; // مدة الأنميشن بالمللي ثانية
    const stepTime = Math.abs(Math.floor(duration / target));

    const timer = setInterval(() => {
      count++;
      counter.textContent = count;
      if (count >= target) {
        clearInterval(timer);
      }
    }, stepTime);
  }

  // إنشاء Intersection Observer
  const observer = new IntersectionObserver(
    (entries, obs) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          animateCounter(entry.target); // شغل العد للعنصر
          obs.unobserve(entry.target);   // أوقف المراقبة بعد التشغيل مرة واحدة
        }
      });
    },
    { threshold: 0.3 } // 30% من العنصر يجب أن يظهر لتشغيل العد
  );

  // مراقبة كل counter
  counters.forEach(counter => observer.observe(counter));
});
