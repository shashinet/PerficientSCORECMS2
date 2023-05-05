// @ts-check
import React from 'react';

/**
 *
 * @returns {{width: number, height: number}}
 */

export default function useWindowSize() {
  const [dimensions, setDimensions] = React.useState({
    width: window.innerWidth,
    height: window.innerHeight,
  });

  const debounce = (fn, ms) => {
    let timer;
    return () => {
      clearTimeout(timer);
      timer = setTimeout(() => {
        // eslint-disable-next-line prefer-rest-params
        fn.apply(this, arguments);
      }, ms);
    };
  };

  const updateDimensions = () => {
    setDimensions({
      width: window.innerWidth,
      height: window.innerHeight,
    });
  };

  React.useEffect(() => {
    window.addEventListener('resize', debounce(updateDimensions, 200));

    return () => {
      window.removeEventListener('resize', debounce(updateDimensions, 200));
    };
  }, []);

  return dimensions;
}
