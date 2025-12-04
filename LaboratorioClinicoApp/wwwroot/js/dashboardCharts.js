let examChart = null;
let citasChart = null;

window.drawExamChart = (pendiente, completo, cancelado) => {

    const ctx = document.getElementById("examChart").getContext("2d");

    if (examChart) examChart.destroy();

    examChart = new Chart(ctx, {
        type: "bar",
        data: {
            labels: ["Pendiente", "Completado", "Cancelado"],
            datasets: [{
                label: "Exámenes",
                data: [pendiente, completo, cancelado],
                backgroundColor: [
                    "#facc15", // amarillo
                    "#4ade80", // verde
                    "#f87171"  // rojo
                ],
                borderRadius: 8
            }]
        },
        options: {
            responsive: true,
            animation: {
                duration: 900,
                easing: "easeOutQuart"
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: { color: "#ddd" }
                },
                x: {
                    grid: { display: false }
                }
            }
        }
    });
};

window.drawCitasChart = (confirmadas, pendientes, completadas, canceladas) => {

    const ctx = document.getElementById("citasChart").getContext("2d");

    if (citasChart) citasChart.destroy();

    citasChart = new Chart(ctx, {
        type: "bar",
        data: {
            labels: ["Confirmadas", "Pendientes", "Completadas", "Canceladas"],
            datasets: [{
                label: "Citas",
                data: [confirmadas, pendientes, completadas, canceladas],
                backgroundColor: [
                    "#3b82f6", // azul
                    "#a78bfa", // morado claro
                    "#4ade80", // verde
                    "#f87171"  // rojo
                ],
                borderRadius: 8
            }]
        },
        options: {
            responsive: true,
            animation: {
                duration: 900,
                easing: "easeOutQuart"
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: { color: "#ddd" }
                },
                x: {
                    grid: { display: false }
                }
            }
        }
    });
};
