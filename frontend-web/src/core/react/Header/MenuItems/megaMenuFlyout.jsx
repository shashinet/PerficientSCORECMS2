import React from 'react';
import PropTypes from 'prop-types';

// children
import MenuList from './menuList';
import Tile from '../../Tile';

// hooks
import useOutsideClick from '../../../hooks/useOutsideClicks';
import useWindowSize from '../../../hooks/useWindowSize';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function MegaMenuFlyout(props) {
  const { block } = props;
  const {
    ref,
    isComponentVisible,
    setIsComponentVisible,
  } = useOutsideClick();
  const { width } = useWindowSize();
  const menuOpen = React.useRef(null);

  React.useEffect(() => {
    if (width < 1260) return;
    if (isComponentVisible && block.navigationPanels.length < 2) {
      menuOpen.current.style.left = `${ref.current.offsetLeft + 20}px`;
    }
    if (!menuOpen.current.nextSibling) {
      menuOpen.current.style.left = 'auto';
      menuOpen.current.style.right = '0';
    }
  }, [isComponentVisible]);

  return (
    <>
      <button
        data-id={block.title.toLowerCase()
          .replace(/\s/g, '-')}
        className={[
          `${isComponentVisible ? 'nav-item-dropdown open' : 'nav-item-dropdown'}`,
          ...block.globalStyle,
        ].join(' ')}
        aria-expanded={isComponentVisible}
        aria-controls={block.title.toLowerCase()
          .replace(/\s/g, '-')}
        aria-label="Open Toggle sub-navigation"
        onClick={() => setIsComponentVisible(!isComponentVisible)}
        type="button"
        ref={ref}
      >
        {block.title}
        {block.iconSrc ? <span className="nav-icon">{block.iconSrc}</span> : (
          <span className="caret">
            {width > 1200 ? (
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="10"
                height="6"
                viewBox="0 0 10 6"
                fill="none"
              >
                <path
                  d="M0.75 0.87677L4.75 4.87677L8.75 0.87677"
                  stroke="#cc1f20"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
              </svg>
            ) : (
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="8"
                height="15"
                viewBox="0 0 8 15"
                fill="none"
              >
                <path
                  d="M1 13.3669L7 7.36688L1 1.36688"
                  stroke="#cc1f20"
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
            )}
          </span>
        )}
        <span className="visually-hidden">toggle menu</span>
      </button>
      <div
        id={block.title.toLowerCase()
          .replace(/\s/g, '-')}
        ref={menuOpen}
        className={[
          'megamenu-content',
          block.navigationPanels.length > 1 ? 'megamenu-wide' : 'megamenu-narrow',
          ...block.globalStyle, isComponentVisible ? 'slide-in' : 'slide-out',
        ].join(' ')}
        aria-hidden={isComponentVisible ? 'false' : 'true'}
        role="navigation"
        aria-label={block.title.toLowerCase()
          .replace(/\s/g, '-')}
      >
        <div className="subnav container">
          <button
            className="utility-nav-close"
            onClick={() => setIsComponentVisible(false)}
            type="button"
          >
            {block.title}
            {width > 1200 ? (
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="21"
                height="20"
                viewBox="0 0 21 20"
                fill="none"
              >
                <path
                  fillRule="evenodd"
                  clipRule="evenodd"
                  d="M1.8269 0.381343C1.48214 0.0396339 0.923163 0.0396339 0.578398 0.381343C0.233633 0.723051 0.233633 1.27707 0.578398 1.61878L9.03465 10.0001L0.578398 18.3813C0.233633 18.7231 0.233633 19.2771 0.578398 19.6188C0.923163 19.9605 1.48214 19.9605 1.8269 19.6188L10.2832 11.2375L18.7394 19.6188C19.0842 19.9605 19.6431 19.9605 19.9879 19.6188C20.3327 19.2771 20.3327 18.7231 19.9879 18.3813L11.5317 10.0001L19.9879 1.61878C20.3327 1.27707 20.3327 0.723051 19.9879 0.381343C19.6431 0.0396339 19.0842 0.0396339 18.7394 0.381343L10.2832 8.76262L1.8269 0.381343Z"
                  fill="white"
                />
              </svg>
            ) : (
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="8"
                height="15"
                viewBox="0 0 8 15"
                fill="none"
              >
                <path
                  d="M1 13.3669L7 7.36688L1 1.36688"
                  stroke="#cc1f20"
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
            )}
          </button>
          {React.Children.toArray(block.navigationPanels.map((col) => (
            <div className="column">
              {React.Children.toArray(col.columnContent.map((columns) => (
                <>
                  {(() => {
                    switch (columns.contentType) {
                      case 'MenuList':
                        return <MenuList block={columns} />;
                      case 'Tile':
                        return <Tile block={columns} />;
                      default:
                        return null;
                    }
                  })()}
                </>
              )))}
            </div>
          )))}
        </div>
      </div>
    </>
  );
}

MegaMenuFlyout.propTypes = {
  block: PropTypes.shape({
    globalStyle: PropTypes.arrayOf(PropTypes.string),
    iconSrc: PropTypes.string,
    title: PropTypes.string.isRequired,
    navigationPanels: PropTypes.arrayOf(
      PropTypes.shape({
        contentType: PropTypes.string,
        id: PropTypes.string,
        globalStyle: PropTypes.arrayOf(PropTypes.string),
        columnContent: PropTypes.arrayOf(
          PropTypes.shape({
            contentType: PropTypes.string,
          }),
        ),
      }),
    ),
  }),
};

MegaMenuFlyout.defaultProps = {
  block: {
    globalStyle: [],
    iconSrc: '',
    title: '',
    navigationPanels: [],
  },
};
