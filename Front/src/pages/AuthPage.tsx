import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import UserRoleTabs from "@/features/Auth/components/UserRoleTabs";

export default function AuthPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [userType, setUserType] = useState<"student" | "teacher">("student");

  const loginLabel = userType === "student" ? "RA ou E-mail" : "E-mail";

  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Form submitted:", {
      email,
      password,
      userType,
    });
  };

  const userTabs = [
    { userName: "Aluno", userType: "student" },
    { userName: "Professor", userType: "teacher" },
  ]

  return (
    <div className="flex flex-col justify-center items-center min-h-screen bg-white">
      <div className="w-full max-w-[450px] p-5 text-center">
        <div className="mb-4 text-[#003366] text-2xl font-bold">
          <span>Cooks Academy</span>
        </div>
        <div>
          <h1 className="text-xl font-semibold text-[#333333]">Portal do Aluno</h1>
          <p className="text-sm text-[#666666]">Bem-vindo à sua área acadêmica</p>
        </div>
      </div>

      <div className="flex flex-col w-full max-w-[450px] px-5 box-border">
        {/* {Tabs} */}
        <div className="flex p-1 mb-6 bg-gray-100 rounded-lg">
          {userTabs.map((tab) => (
            <UserRoleTabs 
              key={tab.userType} 
              userName={tab.userName} 
              isActive={userType === tab.userType}
              onClick={() => setUserType(tab.userType as "student" | "teacher")}
            />
          ))}
        </div>

        <form className="flex flex-col gap-4" onSubmit={onSubmit}>
          <div className="flex flex-col gap-1.5">
            <label htmlFor="login" className="text-sm font-medium text-gray-700">
              {loginLabel}
            </label>
            <Input
              id="login"
              type="text"
              placeholder={loginLabel}
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          
          <div className="flex flex-col gap-1.5">
            <label htmlFor="password" className="text-sm font-medium text-gray-700">
              Senha
            </label>
            <Input
              id="password"
              type="password"
              placeholder="Senha"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>

          <Button type="submit" className="w-full mt-2 bg-[#003366] hover:bg-[#002244] text-white cursor-pointer">
            Acessar
          </Button>
        </form>

        <div className="mt-auto pt-8 pb-4 text-center flex flex-col items-center gap-2">
          <a href="#" className="text-sm text-[#003366] hover:underline">
            Precisa de ajuda? Fale conosco
          </a>
          <p className="text-[10px] text-gray-400">
            © Universidade Federal do Cooks - UFCooks
          </p>
        </div>
      </div>
    </div>
  );
}
