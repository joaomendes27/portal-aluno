

interface UserRoleTabsProps {
    userName: string;
    isActive: boolean;
    onClick: () => void;
}

export default function UserRoleTabs({userName, isActive, onClick }: UserRoleTabsProps) {
  return (
    <button
        type="button"
        className={`flex-1 py-2 text-sm font-medium rounded-md transition-all cursor-pointer ${
        isActive
            ? "bg-white text-gray-900 shadow-sm"
            : "text-gray-500 hover:text-gray-900"
        }`}
        onClick={onClick}
    >
        {userName}
    </button>
  );
}
