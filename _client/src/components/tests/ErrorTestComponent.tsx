import React, { useState } from "react";
import { Modal } from "../Modal"; // Make sure this import matches the actual file location
import { ReactComponent as ErrorIcon } from "assets/icons/error.svg";
import { ReactComponent as NotFoundIcon } from "assets/icons/not-found.svg";
import { ReactComponent as ServerErrorIcon } from "assets/icons/server-error.svg";
import { ReactComponent as UnknownErrorIcon } from "assets/icons/unknown-error.svg";

const ErrorTestComponent = () => {
	const [showError, setShowError] = useState(false);
	const [showNotFound, setShowNotFound] = useState(false);
	const [showServerError, setShowServerError] = useState(false);
	const [showUnknownError, setShowUnknownError] = useState(false);

	type ErrorType =
		| "showError"
		| "showNotFound"
		| "showServerError"
		| "showUnknownError";

	const triggerError = (errorType: ErrorType) => {
		setShowError(false);
		setShowNotFound(false);
		setShowServerError(false);
		setShowUnknownError(false);

		switch (errorType) {
			case "showError":
				setShowError(true);
				break;
			case "showNotFound":
				setShowNotFound(true);
				break;
			case "showServerError":
				setShowServerError(true);
				break;
			case "showUnknownError":
				setShowUnknownError(true);
				break;
			default:
				break;
		}
	};

	const clearErrors = () => {
		console.log("Clearing all errors");
		setShowError(false);
		setShowNotFound(false);
		setShowServerError(false);
		setShowUnknownError(false);
	};
	console.log("showError:", showError);
	console.log("showNotFound:", showNotFound);
	console.log("showServerError:", showServerError);
	console.log("showUnknownError:", showUnknownError);

	return (
		<div>
			<button onClick={() => triggerError("showError")}>Trigger Error</button>
			<button onClick={() => triggerError("showNotFound")}>
				Trigger Not Found
			</button>
			<button onClick={() => triggerError("showServerError")}>
				Trigger Server Error
			</button>
			<button onClick={() => triggerError("showUnknownError")}>
				Trigger Unknown Error
			</button>
			<button onClick={clearErrors}>Clear Errors</button>

			{showError && (
				<Modal
					show={showError}
					title="Error"
					message="An error occurred."
					onClose={() => setShowError(false)}
					IconComponent={ErrorIcon}
				/>
			)}
			{showNotFound && (
				<Modal
					show={showNotFound}
					title="Not Found"
					message="The requested resource was not found."
					onClose={() => setShowNotFound(false)}
					IconComponent={NotFoundIcon}
				/>
			)}
			{showServerError && (
				<Modal
					show={showServerError}
					title="Server Error"
					message="A server error occurred."
					onClose={() => setShowServerError(false)}
					IconComponent={ServerErrorIcon}
				/>
			)}
			{showUnknownError && (
				<Modal
					show={showUnknownError}
					title="Unknown Error"
					message="An unknown error occurred."
					onClose={() => setShowUnknownError(false)}
					IconComponent={UnknownErrorIcon}
				/>
			)}
		</div>
	);
};

export default ErrorTestComponent;
