/* eslint-disable no-shadow */
import React from 'react';
import PropTypes from 'prop-types';

// hooks
import useOutsideClick from '../../hooks/useOutsideClicks';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */
export default function Index(props) {
  const { dropDownMenuItems } = props;

  const { ref, isComponentVisible, setIsComponentVisible } = useOutsideClick(false);

  return (
    <div
      ref={ref}
      style={{
        color: dropDownMenuItems.textColor,

        background: dropDownMenuItems.backgroundColor,
      }}
      className={isComponentVisible ? 'nav-item-dropdown open' : 'nav-item-dropdown'}
    >
      <button
        type="button"
        aria-haspopup="true"
        aria-label="Open toggle dropdown nav"
        // eslint-disable-next-line no-shadow
        onClick={
          () =>
            // eslint-disable-next-line implicit-arrow-linebreak
            setIsComponentVisible((isComponentVisible) => !isComponentVisible)
          // eslint-disable-next-line react/jsx-curly-newline
        }
      >
        <span className="visually-hidden">toggle menu</span>
        <span>{dropDownMenuItems.title}</span>
        <span className="caret">
          <svg viewBox="0 0 24 24">
            <path
              d="M15.8 12L7.1 3.3c-.5-.5-.5-1.3 0-1.8s1.3-.5 1.8 0L19.3 12 8.9 22.4c-.5.5-1.3.5-1.8 0s-.5-1.3 0-1.8l8.7-8.6z"
            />
          </svg>
        </span>
      </button>
      <ul className="submenu-list" role="menu">
        {dropDownMenuItems.childPages.map((item, index) => (
          <li className="submenu-list-item" key={index}>
            <a href={item.link} role="menuitem" target={`${item.openInNewWindow ? '_blank' : '_self'}`}>
              {item.title}
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
}

Index.propTypes = {
  dropDownMenuItems: PropTypes.shape({
    title: PropTypes.string.isRequired,
    textColor: PropTypes.string,
    backgroundColor: PropTypes.string,
    childPages: PropTypes.arrayOf(
      PropTypes.shape({
        title: PropTypes.string,
        link: PropTypes.string,
        openInNewWindow: PropTypes.bool,
      }).isRequired,
    ),
  }),
};

Index.defaultProps = {
  dropDownMenuItems: {
    textColor: '#FFFFFF',
    backgroundColor: '#003B5C',
  },
};
