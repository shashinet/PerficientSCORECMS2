// @ts-check
import React from 'react';

/**
 *
 // eslint-disable-next-line max-len
 * @returns {{
 * ref: React.MutableRefObject<null>,
 * isComponentVisible: boolean, setIsComponentVisible:
 * (value: (((prevState: boolean) => boolean)
* | boolean)) => void}}
 */

export default function useOutsideClick() {
  const [isComponentVisible, setIsComponentVisible] = React.useState(false);
  const ref = React.useRef(null);
  const handleHideDropdown = (event) => {
    if (event.key === 'Escape') {
      // eslint-disable-next-line no-shadow
      setIsComponentVisible(false);
    }
  };

  const handleClickOutside = (event) => {
    if (ref.current && !ref.current.contains(event.target)) {
      setIsComponentVisible(false);
    }
  };

  React.useEffect(() => {
    document.addEventListener('keydown', handleHideDropdown, true);
    document.addEventListener('click', handleClickOutside, true);
    return () => {
      document.removeEventListener('keydown', handleHideDropdown, true);
      document.removeEventListener('click', handleClickOutside, true);
    };
  });

  return { ref, isComponentVisible, setIsComponentVisible };
}
