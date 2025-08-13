import { BrowserMultiFormatReader, DecodeHintType, BarcodeFormat , NotFoundException } from "@zxing/library";
import axios from "axios";
import { useEffect, useRef } from "react";
import { ToastContainer, toast } from 'react-toastify';
const API_URL = import.meta.env.VITE_API_URL;

function BarcodeScanner() {
  const videoRef = useRef(null);
  const codeReader = useRef(null);
  const lastScanAtRef = useRef(0);
    const lastCodeRef = useRef("");
    const COOLDOWN_MS = 1500;

  const handleScan = async (code) => {

    try {
        var scan = await axios.post(`${API_URL}/Scan`, JSON.stringify(code), {
        headers: {
          "Content-Type": "application/json",
        },
      });

        if (scan.status !== 200) {
            errorToast(scan.data || "Error saving barcode");
        return;
        }
        successToast('Barcode saved successfully!');

    } catch (error) {
        errorToast(error.response?.data || "Error saving barcode");

      console.error("Error saving barcode:", error);
    }
    };

    const errorToast = (data) => {
        toast.error(data);

    }

    const successToast = (data) => {
        toast.success(data);

    }

    useEffect(() => {
        // Create a Map of hints
        const hints = new Map();

        // Limit to EAN-13 format
        hints.set(DecodeHintType.POSSIBLE_FORMATS, [BarcodeFormat.EAN_13]);

        // Optional: Try harder if barcode is hard to read
        hints.set(DecodeHintType.TRY_HARDER, true);
        codeReader.current = new BrowserMultiFormatReader(hints);
    codeReader.current
      .listVideoInputDevices()
      .then((videoInputDevices) => {
        const firstDeviceId = videoInputDevices[0]?.deviceId;

        codeReader.current.decodeFromVideoDevice(
          firstDeviceId,
          videoRef.current,
          (result, err) => {
              if (result) {
                  const code = result.getText();
                  const now = Date.now();
                  if (code === lastCodeRef.current && now - lastScanAtRef.current < COOLDOWN_MS) return;

                  lastCodeRef.current = code;
                  lastScanAtRef.current = now;
              handleScan(result.getText());
            }
            if (err && !(err instanceof NotFoundException)) {
              console.error("Error decoding barcode:", err);
                errorToast("Error decoding barcode. Please try again.");
            }

          }
        );
      })
      .catch((err) => {
        console.error(err);
          errorToast("Camera access denied or not available.");
      });

    return () => stopScanner();
  }, []);

  const stopScanner = () => {
    codeReader.current.reset();
  };

  return (
      <div>
          <ToastContainer position="buttom-right" autoClose={3000}
              pauseOnHover={false}
              pauseOnFocusLoss={false} />
      <button onClick={() => stopScanner()} style={{ marginBottom: "10px" }}>
        Stop Scanner
          </button>

          <div style={{ width: "100%", height: "15%" }}>
      <video ref={videoRef} style={{ width: "100%", height: "15%" }} /></div>
    </div>
  );
}

export default BarcodeScanner;
