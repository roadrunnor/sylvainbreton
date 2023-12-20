import React from 'react';
import { ModalProps } from '../models/ModalProps'; // Assurez-vous que le chemin est correct
import { ReactComponent as ErrorIcon } from '../icons/error.svg';
import { ReactComponent as NotFoundIcon } from '../icons/not-found.svg';
import { ReactComponent as ServerErrorIcon } from '../icons/server-error.svg';
import { ReactComponent as UnknownErrorIcon } from '../icons/unknown-error.svg';

import './modal.scss';

export const Modal: React.FC<ModalProps> = ({ show, title, message, onClose, icon }) => {
  if (!show) {
    return null;
  }

  let IconComponent;
  switch (icon) {
    case 'error':
      IconComponent = <ErrorIcon />;
      break;
    case 'not_found':
      IconComponent = <NotFoundIcon />;
      break;
    case 'server_error':
      IconComponent = <ServerErrorIcon />;
      break;
    case 'unknown_error':
      IconComponent = <UnknownErrorIcon />;
      break;
    // Ajoutez d'autres cas si nécessaire
    default:
      IconComponent = null;
  }

  return (
    <div className="modal" onClick={onClose}>
      <div className="modal-content" onClick={e => e.stopPropagation()}>
        <span className="close" onClick={onClose}>&times;</span>
        {IconComponent}
        <h2>{title}</h2>
        <p>{message}</p>
        <button onClick={onClose}>Fermer</button>
      </div>
    </div>
  );
};


