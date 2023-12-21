import React from "react";
import { ModalProps } from "../models/ModalProps"; // Update the path if needed
import { ReactComponent as ErrorIcon } from "assets/icons/error.svg";
import { ReactComponent as NotFoundIcon } from "assets/icons/not-found.svg";
import { ReactComponent as ServerErrorIcon } from "assets/icons/server-error.svg";
import { ReactComponent as UnknownErrorIcon } from "assets/icons/unknown-error.svg";
import "../scss/_modal.scss";

export const Modal: React.FC<ModalProps> = ({
	show,
	title,
	message,
	onClose,
	IconComponent,
}) => {
	// Log the modal props to the console
	console.log("Modal props", { show, title, message, IconComponent });

	if (!show) {
		return null;
	}

	return (
		<div className="modal" onClick={onClose}>
			<div className="modal-content" onClick={(e) => e.stopPropagation()}>
				<span className="close" onClick={onClose}>
					&times;
				</span>
				{IconComponent && <IconComponent className="svg-icon" />}
				{/* Render the SVG with the class directly */}
				<h2>{title}</h2>
				<p>{message}</p>
				<button onClick={onClose}>Close</button>
			</div>
		</div>
	);
};
