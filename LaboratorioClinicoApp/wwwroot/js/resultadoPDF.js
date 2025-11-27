window.GenerarPDFResultadoClinico = async function (data) {

    const { jsPDF } = window.jspdf;
    const pdf = new jsPDF();

    const pageWidth = pdf.internal.pageSize.width;
    const pageHeight = pdf.internal.pageSize.height;

    // Colores
    const azul = "#003366";
    const gris = "#555555";

    // ------------------ ENCABEZADO ESTILIZADO ------------------
    pdf.setFillColor(0, 51, 102); // azul oscuro
    pdf.rect(0, 0, pageWidth, 20, "F");

    pdf.setFontSize(16);
    pdf.setTextColor(255, 255, 255);
    pdf.text("Laboratorio Clínico", 10, 13);

    // ------------------ TÍTULO PRINCIPAL ------------------
    pdf.setFontSize(18);
    pdf.setTextColor(0, 0, 0);
    pdf.text("Resultado Clínico", 10, 35);

    pdf.setDrawColor(0, 51, 102);
    pdf.setLineWidth(0.8);
    pdf.line(10, 38, pageWidth - 10, 38);

    // ------------------ INFORMACIÓN PRINCIPAL ------------------
    pdf.setFontSize(12);
    pdf.setTextColor(0, 0, 0);

    // Paciente
    pdf.text("Paciente:", 10, 55);
    pdf.setTextColor(gris);
    pdf.text(`${data.paciente}`, 45, 55);

    pdf.setTextColor(0, 0, 0);
    pdf.text("Doctor:", 10, 65);
    pdf.setTextColor(gris);
    pdf.text(`Dr. ${data.doctor}`, 45, 65);

    pdf.setTextColor(0, 0, 0);
    pdf.text("Tipo de Examen:", 10, 75);
    pdf.setTextColor(gris);
    pdf.text(`${data.examen}`, 45, 75);

    pdf.setTextColor(0, 0, 0);
    pdf.text("Fecha de Emisión:", 10, 85);
    pdf.setTextColor(gris);
    pdf.text(`${data.fecha}`, 45, 85);

    pdf.setTextColor(0, 0, 0);
    pdf.text("Estado:", 10, 95);
    pdf.setTextColor(gris);
    pdf.text(`${data.estado}`, 45, 95);

    // ------------------ DETALLE DEL RESULTADO ------------------
    pdf.setFontSize(14);
    pdf.setTextColor(0, 0, 0);
    pdf.text("Detalle del Resultado", 10, 115);

    pdf.setDrawColor(0, 51, 102);
    pdf.line(10, 118, pageWidth - 10, 118);

    pdf.setFontSize(12);
    pdf.setTextColor(gris);
    pdf.text(data.detalle, 10, 130, { maxWidth: pageWidth - 20 });

    // ------------------ FIRMA DEL DOCTOR ------------------
    pdf.setTextColor(0, 0, 0);
    pdf.setFontSize(12);

    // Línea de firma
    pdf.line(10, pageHeight - 30, 70, pageHeight - 30);

    pdf.text(`Dr. ${data.doctor}`, 10, pageHeight - 20);
    pdf.setTextColor(gris);
    pdf.text("Firma del Médico Responsable", 10, pageHeight - 14);

    // ------------------ PIE DE PÁGINA ------------------
    pdf.setFontSize(10);
    pdf.setTextColor(120);
    pdf.text(
        "© Laboratorio Clínico - Sistema de Consultas y Exámenes",
        10,
        pageHeight - 5
    );

    // ------------------ IMPRIMIR/ABRIR PDF ------------------
    pdf.autoPrint();
    pdf.output("dataurlnewwindow");
};
