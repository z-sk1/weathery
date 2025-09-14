function copyData() {
    const resultDiv = document.getElementById("result");
    const copyBtn = document.getElementById("copyBtn");
    const city = document.getElementById("cityInput").value;
    let textToCopy = "";

    textToCopy += city;

    for (let node of resultDiv.childNodes) {
        if (node.nodeType === Node.ELEMENT_NODE && node.tagName === "BUTTON") {
            continue;
        }

        if (node.nodeType === Node.TEXT_NODE || node.nodeType === Node.ELEMENT_NODE) {
            textToCopy += node.textContent;
        }
    }

    navigator.clipboard.writeText(textToCopy.trim())
        .then(() => {
            copyBtn.innerText = "Copied!";
            setTimeout(() => {copyBtn.innerText = "Copy"}, 3000);
        })
        .catch(err => {
            alert("Failed to copy! Error: ", err);
        });
}