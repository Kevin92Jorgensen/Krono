import { BrowserMultiFormatReader, NotFoundException } from "@zxing/library";
import axios from "axios";
import { useEffect, useRef, useState } from "react";

function BarcodeScanner(params) {
  const videoRef = useRef(null);
  const codeReader = useRef(null);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleScan = async (code) => {
    console.log("Scanned barcode:", code);

    try {
      var scan = await axios.post("https://localhost:7076/Scan", JSON.stringify(code), {
        headers: {
          "Content-Type": "application/json",
        },
      });

      if(!scan.status || scan.status !== 200) {
        setError(scan.data || "Error saving barcode");
        setSuccess(null);
        return;
      }
      console.log("scan", scan);
      setSuccess("Barcode saved successfully!");
      setError(null);
    } catch (error) {
      setError(error.response?.data || "Error saving barcode");
      setSuccess(null);
      console.error("Error saving barcode:", error);
    }
  };

  useEffect(() => {

    codeReader.current = new BrowserMultiFormatReader();

    codeReader.current
      .listVideoInputDevices()
      .then((videoInputDevices) => {
        const firstDeviceId = videoInputDevices[0]?.deviceId;

        codeReader.current.decodeFromVideoDevice(
          firstDeviceId,
          videoRef.current,
          (result, err) => {
            if (result) {
              handleScan(result.getText());
            }
            if (err && !(err instanceof NotFoundException)) {
              console.error("Error decoding barcode:", err);
              setError("Error decoding barcode. Please try again.");
            }

          }
        );
      })
      .catch((err) => {
        console.error(err);
        setError("Camera access denied or not available.");
      });

    return () => stopScanner();
  }, []);

  const stopScanner = () => {
    codeReader.current.reset();
  };

  return (
    <div>
      {error && <p style={{ color: "red" }}>{error}</p>}

        {success && <p style={{ color: "green" }}>{success}</p>}
      <button onClick={() => stopScanner()} style={{ marginBottom: "10px" }}>
        Stop Scanner
      </button>
      <video ref={videoRef} style={{ width: "100%", height: "520px" }} />
    </div>
  );
}

export default BarcodeScanner;
